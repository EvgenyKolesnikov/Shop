import axios from "axios"


    export async function GetRequest()
    {
        let res = await axios.get(`https://localhost:7222/Shop/GetProducts`);
        let data = res.data;
        
        console.log(data);
        return data;
    }





    // export const productsAPI = {
    //     getProducts() {
    //         return axios.get(`https://localhost:7222/Shop/GetProducts`).then(response => response.data.data);
    //     }
        // createProduct(product) {
        //     return instance.post(`CreateProduct`, {...product }).then(response => response.data);
        // },
        // deleteProduct(id) {
        //     return instance.delete(`DeleteProduct`, { data: { id: id }}, );
        // },
        // updateProduct(product) {
        //     return instance.put(`UpdateProduct`, {...product}).then(response => response.data.data);
        // }
   // }



    
