import { autoinject, bindable } from 'aurelia-framework';
import { ValidationRules, ValidationControllerFactory} from 'aurelia-validation'
import {HttpClient, json} from 'aurelia-fetch-client';
import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons'
import {DialogService} from 'aurelia-dialog';
import {NotificationService} from 'aurelia-notify';
import * as moment from 'moment'
interface Asset {
  id : number,
  name : string,
  department : string,
  country : string,
  email : string,
  date : Date,
  broken : boolean,
  departmentCode: number
}

@autoinject
export class App {
  editMode = false;
  controller;
  httpClient;
  dialogService;
  notificationService;
  tableData: Asset[] = []
  @bindable id;
  @bindable name;
  @bindable department;
  @bindable country;
  @bindable email;
  @bindable date;
  @bindable broken;
  trash = faTrash;
  edit = faEdit;

  constructor(http: HttpClient, 
    controllerFactory : ValidationControllerFactory,
    dialogService : DialogService,
    notificationService : NotificationService){
    this.httpClient = http.configure(c =>{
      c.withBaseUrl('https://localhost:5001/api/')
      .withDefaults({
        headers: {
          'Accept': 'application/json',
          'Content-type' : 'application/json',
          'Access-Control-Allow-Origin':'*'
        }
      })
    }) ; 
    this.controller = controllerFactory.createForCurrentScope();
    this.dialogService = dialogService;
    this.notificationService = notificationService;
  }

  getAsset(id){
    if ( this.editMode ){
      this.tableData.splice(this.tableData.findIndex(function(i){
        return i.id === id;
      }), 1);
    }
    this.httpClient.fetch('Asset/' + id)
    .then(response => response.json())
    .then(data => {
      this.tableData.push({
        id : data.id,
        name : data.assetName,
        department : data.departmentName,
        country : data.countryOfDepartment,
        email: data.emailOfDepartment,
        date: data.purchaseDate,
        broken : data.broken,
        departmentCode : data.departmentCode
      });
    });
  }

  addAsset(){
    let model = {
      "id" : this.id,
      "assetName": this.name,
      "department": Number(this.department),
      "countryOfDepartment": this.country,
      "emailOfDepartment": this.email,
      "purchaseDate": this.date,
      "broken": this.broken
    }

    if ( this.editMode == true ){
      this.httpClient.fetch('Asset/',{
        method: 'put',
        body: json(model)
      })
      .then(response => {
        if ( response.ok ){
          this.notificationService.success('Successfully Updated');
          this.getAsset(model.id)
          this.editMode = false

          this.id = null
          this.name = null
          this.department = null
          this.email = null
          this.broken = null
          this.country = null
          this.date = null
        }else{
          this.notificationService.danger('Update Failed');
        }
      })
    }else{
      this.httpClient.fetch('Asset/',{
        method: 'post',
        body: json(model)
      })
      .then(response => response.json())
      .then(data => {
        if ( data.id ){
          this.notificationService.success('Successfully Added');
          this.getAsset(data.id)

          this.id = null
          this.name = null
          this.department = null
          this.email = null
          this.broken = null
          this.country = null
          this.date = null
  
        }else{
          this.notificationService.danger(data);
        }
      }); 
    }
  }

  deleteAsset(id){
    this.httpClient.fetch('Asset?id='+id,{
      method: 'delete'
    })
    .then(data => {
      if ( data.ok ){
        this.tableData.splice(this.tableData.findIndex(function(i){
          return i.id === id;
        }), 1);
        this.notificationService.success("Successfully deleted");
      }else{
        this.notificationService.danger(data);
      }
    });
  }
  
  editAsset(input){
    this.editMode = true;

    this.id = Number(input.id) 
    this.name = input.name
    this.email = input.email
    this.country = input.country
    this.date = moment(input.date).format('yyyy-MM-DD')
    this.department = input.departmentCode
    this.broken = input.broken
  }

}
