import { test, expect } from '@playwright/test'

test('homepage laadt en toont de navigatiebalk', async ({ page }) => {
  await page.goto('/')

  // Navigatiebalk aanwezig
  const navbar = page.locator('.navbar')
  await expect(navbar).toBeVisible()

  // Logo tekst zichtbaar
  await expect(navbar.locator('a').first()).toContainText('VerhuurApp')

  // Navigatielinks aanwezig
  await expect(page.getByRole('link', { name: 'Home' })).toBeVisible()
  await expect(page.getByRole('link', { name: "Auto's" })).toBeVisible()
  await expect(page.getByRole('link', { name: 'Inloggen' })).toBeVisible()
})

test("auto-overzicht toont de pagina met auto's", async ({ page }) => {
  await page.goto('/autos')

  // Paginatitel aanwezig
  await expect(page.locator('h1')).toContainText("Beschikbare auto's")

  // Wacht tot laden klaar is (grid of foutmelding verschijnt)
  await page.waitForSelector('.autos-grid, .autos-pagina__status', { timeout: 10000 })

  const grid = page.locator('.autos-grid')
  const heeftAutos = await grid.isVisible()

  if (heeftAutos) {
    // Als de backend draait: minimaal één auto-kaart zichtbaar
    const kaarten = grid.locator('.auto-card')
    await expect(kaarten.first()).toBeVisible()
  } else {
    // Als de backend niet beschikbaar is: statusmelding zichtbaar
    await expect(page.locator('.autos-pagina__status')).toBeVisible()
  }
})

test('detailpagina laadt via navigatie vanuit het overzicht', async ({ page }) => {
  await page.goto('/autos')

  await page.waitForSelector('.autos-grid, .autos-pagina__status', { timeout: 10000 })

  const grid = page.locator('.autos-grid')
  const heeftAutos = await grid.isVisible()

  if (!heeftAutos) {
    test.skip()
    return
  }

  // Klik op de eerste auto
  await grid.locator('.btn--details').first().click()

  // Controleer dat de detailpagina geladen is
  await page.waitForSelector('.detail-kaart, .status', { timeout: 10000 })

  const detailKaart = page.locator('.detail-kaart')
  if (await detailKaart.isVisible()) {
    // Titel bevat merk en model
    await expect(page.locator('.detail-kaart__inhoud h1')).not.toBeEmpty()

    // Prijs per dag aanwezig in de specs-tabel
    await expect(page.locator('.specs-tabel')).toBeVisible()

    // Terug-link aanwezig
    await expect(page.locator('.terug-link')).toBeVisible()
  }
})

test('loginpagina toont het inlogformulier', async ({ page }) => {
  await page.goto('/login')

  await expect(page.locator('h1')).toContainText('Inloggen')
  await expect(page.locator('input[type="email"]')).toBeVisible()
  await expect(page.locator('input[type="password"]')).toBeVisible()
  await expect(page.locator('button[type="submit"]')).toBeVisible()
})
