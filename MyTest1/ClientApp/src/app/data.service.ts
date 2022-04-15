import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Employee } from './Employee';

@Injectable()
export class DataService {

  private url = "/api/products";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  }

  getProducts() {
    return this.http.get(this.url);
  }

  getProduct(id: number) {
    return this.http.get(this.url + '/' + id);
  }

  createProduct(employee: Employee) {
    return this.http.post(this.url, employee);
  }
  updateProduct(employee: Employee) {

    return this.http.put(this.url, employee);
  }
  deleteProduct(id: number) {
    return this.http.delete(this.url + '/' + id);
  }
}
