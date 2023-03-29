export class TimeHistoryModel {

    projectId: String
    startDate: Date
    endDate: Date
    totalHours: Number

    constructor (projectId: String, startDate: Date, endDate: Date, totalHours?: Number ){
       this.projectId = projectId
       this.startDate = startDate
       this.endDate = endDate
       this.totalHours = totalHours ?? 0
    }
}