import axios from 'axios'

const baseUrl = 'http://shopyshop.somee.com'

export default class ApiService {

    static async getCategories() {
        try {
            const res = await axios.get(baseUrl + `/Shop/GetCategories`)
            return res.data

        } catch (e) {
            console.log(e);
        }
    }

    static async getProducts() {
        try {
            const res = await axios.get(baseUrl + `/Shop/GetProducts`)
            return res.data

        } catch (e) {
            console.log(e);
        }
    }
}

