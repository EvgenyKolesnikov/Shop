import { render } from '@testing-library/react';
import Navigation from './component/navigation';
import axios from 'axios';
import { useState } from 'react';
import React,{Component} from 'react';




function App() {

 let  state = {
    products: []
  }

  
  let products = axios.get(`http://shopyshop.somee.com/Shop/GetProducts`).then(res =>{
    this.setState({products: res.data})
  })
 
 

  
  
  

  // const products = [
  //   { id: 8, name: "aa", category: 32, price: "123" },
  //   { id: 9, name: "bb", category: 31, price: "333" },
  //   { id: 10, name: "cc", category: 29, price: "222" },
  //   { id: 19, name: "dd", category: 20, price: "444" },
  //   { id: 14, name: "ee", category: 25, price: "222" },
  // ]

  return (
    <div>
        <Navigation />


        <table class="table">
    <thead>
      <tr>
        <th scope="col">#</th>
        <th scope="col">Name</th>
        <th scope="col">category</th>
        <th scope="col">Price</th>
      </tr>
    </thead>
    <tbody>
      {products.map(product => (
        <tr key={product.id}>
          <th scope="row">{product.id}</th>
          <td>{product.name}</td>
          <td>{product.category}</td>
          <td>{product.price}</td>
        </tr>
      ))}
    </tbody>
  </table>
     
    </div>
  );
}

export default App;
