import axios from "axios";
import { startEndpoint } from "./api";

//get Categories
export const getCategories = async () => {
  try {
    let endpoint = startEndpoint + "/categories/get";
    let response = await axios.get(endpoint);
    return response;
  } catch (e) {
    console.log(e);
  }
};
