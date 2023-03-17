import axios from "axios";
import { startEndpoint } from "./api";

//get Websites
export const getWebsites = async () => {
  try {
    let endpoint = startEndpoint + "/websites/get";
    let response = await axios.get(endpoint);
    return response;
  } catch (e) {
    console.log(e);
  }
};
