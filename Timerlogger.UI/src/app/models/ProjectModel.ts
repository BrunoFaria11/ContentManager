export class ProjectModel {

    id: String
    name: String
    deadLine: Date
    timePerWeek: Number
    totalTimeSpent: Number
    isCompleted: Boolean 
    
    constructor (id: String, name: String, deadLine: Date, timePerWeek: Number, totalTimeSpent: Number, isCompleted: Boolean ){
       this.id = id
       this.name = name
       this.deadLine = deadLine
       this.timePerWeek = timePerWeek
       this.totalTimeSpent = totalTimeSpent
       this.isCompleted = isCompleted
    }
}