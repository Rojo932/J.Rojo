import axios from "axios";

let endpoint= "https://api.remotebootcamp.dev/api/friends/"

let add = (payload) => {
    console.log("friendsService.add Executing", payload)
    const config = {
        method: "POST",
        url: `${endpoint}`,
        data: payload,
        crossdomain: true,
        headers: { "Content-Type": "application/json" }
    };
    return axios(config).then((response => {
        payload.id = response.data.item
        return payload
    }));
};

let update = ( payload, id) => {
    console.log("friendsService update executing")
    payload.id = id
    const config = {
        method: "PUT",
        url: `${endpoint}${id}`,
        data: payload,
        crossdomain: true,
        headers: { "Content-Type": "application/json" }
    };
    return axios(config).then(() => {
        return payload
    });
};

let deleteById  = (id) => {
    console.log("friendsService deleteById executing on >> ",id)
    const config = {
        method: "DELETE",
        url: `${endpoint}${id}`,
        crossdomain: true,
        headers: { "Content-Type": "application/json" }
    };
    return axios(config).then(() => id)
};

let getAll = () => {
    console.log("friendsService.getAll >> Executing")
    const config = {
        method: "GET",
        url: `${endpoint}?pageIndex=0&pageSize=8`,
        crossdomain: true,
        headers: { "Content-Type": "application/json" }
    };
    return axios(config)
};

let search = (query) => {
    console.log("friendsService.search >> Executing")
    const config = {
        method: "GET",
        url: `${endpoint}search?pageIndex=0&pageSize=8&q=${query}`,
        crossdomain: true,
        headers: { "Content-Type": "application/json" }
    };
    return axios(config)
};

let Paginate = () => {
    let index = 0;
    let size = 8;
    console.log("friendsService.search >> Executing")
    const config = {
        method: "GET",
        url: `${endpoint}search?pageIndex=${index}&pageSize=${size}`,
        crossdomain: true,
        headers: { "Content-Type": "application/json" }
    };
    return axios(config)
};

const services = {add, update, deleteById, getAll, search, Paginate}
export default services
