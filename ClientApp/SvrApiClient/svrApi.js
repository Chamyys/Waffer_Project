import axios from 'axios'

const api = axios.create({
  baseURL: 'https://localhost:3000/api/proxy/',
})

export default api

export const getWafers = async () => {
  try {
    const response = await api.get('wafer/all')
    return response.data
  } catch (error) {
    console.error(error)
  }
}
export const getProcessByWafer = async (waferId) => {
  try {
    const response = await api.get('GetProcessByWafer/getProcessById', {
      params: {
        number: waferId,
      },
    })
    return response.data
  } catch (error) {
    console.error(error)
  }
}

/*
API  http/192.168.11.10/public_api/

Получение номеров пластин wafer/all

Получить процесс по пластине process/waferId/{waferId:string}

Получить запуск по id процесса parcel/processId/{processId:int}

Получить монитор по номеру пластины dieType/wafer/{waferId:string}

Получить элементы по id монитора element/dieType/id/{dieTypeId:int}

Получить этапы по номеру пластины stage/wafer/{waferId:string}

Получить номера измерений по номеру пластины measurementRecording/getMeasurementRecordingByWaferId?waferId={waferId:string}

Получить файлнеймы по id процесса filegraphic/process/{processId:int}

Получить графики по Id файлнейма filegraphic/graphics/{fileNameId:int}
*/
