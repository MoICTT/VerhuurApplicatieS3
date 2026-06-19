import { onUnmounted } from 'vue'
import * as signalR from '@microsoft/signalr'

const HUB_URL = 'http://localhost:5150/hubs/autos'

export function useAutoHub(onBeschikbaarheidGewijzigd) {
  const connection = new signalR.HubConnectionBuilder()
    .withUrl(HUB_URL)
    .withAutomaticReconnect()
    .build()

  connection.on('AutoBeschikbaarheidGewijzigd', (autoId, beschikbaar) => {
    onBeschikbaarheidGewijzigd(autoId, beschikbaar)
  })

  connection.start().catch((err) => console.warn('SignalR verbinding mislukt:', err))

  onUnmounted(() => connection.stop())

  return connection
}
