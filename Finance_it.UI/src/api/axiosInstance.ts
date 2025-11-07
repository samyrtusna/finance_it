import axios from "axios";
import config from "../../config.json";

const ApiUrl = config.apiurl;

const AxiosService = axios.create({
  baseURL: ApiUrl,
  headers: { "Content-Type": "application/json" },
  withCredentials: true,
});

export default AxiosService;
