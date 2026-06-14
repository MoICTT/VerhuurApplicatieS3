import http from 'k6/http'
import { check, sleep } from 'k6'

// Simuleer 20 gelijktijdige gebruikers gedurende 30 seconden
export const options = {
  vus: 20,
  duration: '30s',
  thresholds: {
    http_req_duration: ['p(95)<500'], // 95% van requests moet onder 500ms blijven
    http_req_failed: ['rate<0.01'],   // minder dan 1% mag mislukken
  },
}

const BASE_URL = 'http://localhost:5150/api'

export default function () {
  // GET /api/autos — meest bezochte endpoint
  const autosRes = http.get(`${BASE_URL}/autos`)
  check(autosRes, {
    'autos: status 200': (r) => r.status === 200,
    'autos: response < 500ms': (r) => r.timings.duration < 500,
    'autos: bevat resultaten': (r) => JSON.parse(r.body).length > 0,
  })

  sleep(1)

  // GET /api/autos/:id — detail van een specifieke auto
  const detailRes = http.get(`${BASE_URL}/autos/1`)
  check(detailRes, {
    'detail: status 200': (r) => r.status === 200,
    'detail: response < 300ms': (r) => r.timings.duration < 300,
  })

  sleep(1)
}
