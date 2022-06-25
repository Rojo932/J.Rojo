import axios from "axios";

let  endpoint =  "https://api.remotebootcamp.dev/api/users/";

let logIn = (payload) => {
  console.log("usersService.logIn running")

  const config = {
    method: "POST",
    url: `${endpoint}login`,
    data: payload,
    withCredentials: true,
    crossdomain: true,
    headers: { "Content-Type": "application/json" }
  };

  return axios(config)
};

let logOut = () => {
  console.log("LogOut running")

  const config = {
    method: "GET",
    url: `${endpoint}logout`,
    withCredentials: true,
    crossdomain: true,
    headers: { "Content-Type": "application/json" }
  };

  return axios(config) 
};


let register = (payload) => {

  const config = {
    method: "POST",
    url: `${endpoint}register`,
    data: payload,
    withCredentials: true,
    crossdomain: true,
    headers: { "Content-Type": "application/json" }
  };

  return axios(config);
};

const services = {logIn, logOut, register}
export default services
