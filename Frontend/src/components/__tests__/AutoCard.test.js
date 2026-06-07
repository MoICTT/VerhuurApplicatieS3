import { mount } from '@vue/test-utils'
import { describe, it, expect } from 'vitest'
import AutoCard from '../AutoCard.vue'

const autoVoorbeeld = {
  id: 1,
  merk: 'Toyota',
  model: 'Yaris',
  bouwjaar: 2022,
  prijsPerDag: 50,
  beschikbaar: true,
  afbeeldingUrl: null,
}

describe('AutoCard', () => {
  it('toont merk en model', () => {
    const wrapper = mount(AutoCard, {
      props: { auto: autoVoorbeeld },
      global: { stubs: { RouterLink: true } },
    })

    expect(wrapper.text()).toContain('Toyota')
    expect(wrapper.text()).toContain('Yaris')
  })

  it('toont de prijs per dag', () => {
    const wrapper = mount(AutoCard, {
      props: { auto: autoVoorbeeld },
      global: { stubs: { RouterLink: true } },
    })

    expect(wrapper.text()).toContain('50')
  })

  it('toont "Beschikbaar" als auto beschikbaar is', () => {
    const wrapper = mount(AutoCard, {
      props: { auto: autoVoorbeeld },
      global: { stubs: { RouterLink: true } },
    })

    expect(wrapper.text()).toContain('Beschikbaar')
  })

  it('toont "Niet beschikbaar" als auto niet beschikbaar is', () => {
    const wrapper = mount(AutoCard, {
      props: { auto: { ...autoVoorbeeld, beschikbaar: false } },
      global: { stubs: { RouterLink: true } },
    })

    expect(wrapper.text()).toContain('Niet beschikbaar')
  })

  it('toont placeholder als er geen afbeelding is', () => {
    const wrapper = mount(AutoCard, {
      props: { auto: autoVoorbeeld },
      global: { stubs: { RouterLink: true } },
    })

    expect(wrapper.find('img').exists()).toBe(false)
  })
})
