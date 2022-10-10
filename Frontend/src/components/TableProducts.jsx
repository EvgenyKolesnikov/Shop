import React, {useState} from 'react';
import axios from 'axios';

const TableProducts = ({products}) => {
    const [isShow, setIsShow] = useState(true)

    const deleteProduct = (id) => {
        console.log(id)
        axios.delete(`https://localhost:7222/AdminPanel/DeleteProduct/${id}`)
        then(res => 
            console.log('Delete', res)
            ).catch(err => console.log(err));
    };

    return (
        <div>
            <h2>
                <button onClick={() => setIsShow(!isShow)}>Список товаров</button>
            </h2>

            {isShow && <table className="table products">
                <thead className="product_col">
                <tr className="product_item product_col">
                    <th>id</th>
                    <th>Продукт</th>
                    <th>Категория</th>
                    <th>Описание</th>
                    <th>Цена</th>
                    <th>Действия</th>
                </tr>
                </thead>
                <tbody className="product_col">
                {products.map((product) => {
                    const {id,name, categoryId, categoryName, features, info, price, rating} = product
                    return (
                        <tr key={id} className="product_item product_col">
                            <td>{id}</td>
                            <td>{name}</td>
                            <td>{categoryName}</td>
                            <td>{info}</td>
                            <td>{price}</td>
                            <button onClick={() => deleteProduct(id)}>Удалить</button>
                        </tr>
                    )
                })}
                </tbody>
            </table>}
        </div>
    );
};

export default TableProducts;