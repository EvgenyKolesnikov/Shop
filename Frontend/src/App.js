import './App.css';
import {useEffect, useState} from "react";
import TableCategories from "./components/TableCategories";
import TableProducts from "./components/TableProducts";
import CreateCategoryForm from "./components/CreateCategoryForm";
import CreateProductForm from "./components/CreateProductForm";
import axios from "axios";
import  ApiService  from "./Api/ApiService";

function App() {
    const [products, setProducts] = useState([])
    const [categories, setCategories] = useState([])

    useEffect(() => {
        fetchCategories()
        fetchProducts();
    }, [])

    async function fetchCategories(){
        const categories = await ApiService.getCategories();
        setCategories(categories)
    }

    async function fetchProducts(){
        const products = await ApiService.getProducts();
        setProducts(products);
    }

    return (
        <div className='container'>
            <div>
                <CreateCategoryForm/>
                <CreateProductForm/>
            </div>

            <TableProducts products={products}/>
            <TableCategories categories={categories}/>
        </div>
    );
}

export default App;
