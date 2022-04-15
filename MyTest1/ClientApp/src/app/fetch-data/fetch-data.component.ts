import { Component, Inject, Injectable, OnInit, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Employee } from '../Employee';

@Injectable()
export class DataService {

  private url: string;
  private adr: string = "Employee";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl;
  }

  getEmployees() {
    return this.http.get<Employee[]>(this.adr);
  }

  getEmployeeOnFilter(employee: Employee) {
    return this.http.post<Employee[]>(this.adr + "/GetOnFilter", employee);
  }

  createEmployee(employee: Employee) {
    return this.http.put(this.adr + "/Insert", employee);
  }

  updateEmployee(employee: Employee) {
    return this.http.put(this.adr + "/Update", employee);
  }

  deleteEmployee(id: number) {
    return this.http.delete(this.adr + '/' + id);
  }
}

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
  styleUrls: ['./fetch-data.component.css'],
  providers: [DataService]
})
export class FetchDataComponent implements OnInit{
  //@ViewChild('childBtn') childBtn: BtnSortComponent;
  edit: boolean = false;
  createB: boolean = false;
  modalWin: boolean = false;
  onDel: boolean = false;

  public employees: Employee[];
  public employee: Employee = new Employee();
  public employeeFilter: Employee = new Employee();

  columns = 5;
  sorter = new Array<boolean>();//в каком порядке сортировать какую колонку
  colSorter = new Array<boolean>();//какая колонка активна для сортировки

  constructor(private dataService: DataService) {
  }

  ngOnInit() {
    for (var i = 0; i < this.columns; i++) {
      this.sorter[i] = false;
    }
    for (var i = 0; i < this.columns; i++) {
      this.colSorter[i] = false;
    }
    this.loadData();
  }

  swichEdit(e: Employee) {
    this.employee = JSON.parse(JSON.stringify(e));
    this.edit = !this.edit;
  }

  loadData() {
    this.dataService.getEmployees().subscribe(result => {
      this.employees = result;
    }, error => console.error(error));
  }

  delete(e: Employee) {
    this.dataService.deleteEmployee(e.id).subscribe(result => this.loadData());
  }

  modalRes(increased: any) {
    if (increased) {
      if (this.onDel) {
        this.delete(this.employee);
      } else {
        this.save();
      }
    } 
    this.onDel = false;
    this.modalWin = false;
  }

  openModal(e?: Employee) {
    if (e != undefined) {
      this.employee = JSON.parse(JSON.stringify(e));
      this.onDel = true;
    }
    this.modalWin = true;
  }

  save() {
    if (this.createB) {
      this.dataService.createEmployee(this.employee)
        .subscribe(() => this.loadData());        
    } else if (this.edit) {
      this.dataService.updateEmployee(this.employee)
        .subscribe(() => this.loadData());
    }
    //обнулить
    this.close()
  }

  close() {
    this.modalWin = false;
    this.edit = false;
    this.createB = false;
    this.employee = new Employee();
  }

  create() {
    this.employee = new Employee();
    this.edit = false;
    this.createB = true;
  }

  filter() {
    this.dataService.getEmployeeOnFilter(this.employeeFilter).subscribe(result => {
      this.employees = result;
    }, error => console.error(error));
  }

  clearFilter() {
    this.employeeFilter = new Employee();
    this.loadData();
  }

  onChangedSort(sorterId: number, increased: any) {
    for (var i = 0; i < this.columns; i++) {
      this.colSorter[i] = false;
    }
    this.colSorter[sorterId] = true;
    this.sorter[sorterId] = increased;
    this.sort();
  }

  sort() {
    this.employees.sort((a, b) => this.changeSort(a, b))
  }

  changeSort(a: Employee, b: Employee): number {
    var properties = Object.keys(a);
    var i: number = properties.length-1;
    var result = 0;

    for (var x = 0; x < this.colSorter.length; x++) {
      if (this.colSorter[x]) {
        var howSort = (this.sorter[x]) ? (a[properties[x + 1]] >= b[properties[x + 1]]) : (a[properties[x + 1]] < b[properties[x + 1]]);
        if (howSort) {
          return -1;
        } else {
          return 1;
        }
        break;
      }
    }
    return result;
  }
}


