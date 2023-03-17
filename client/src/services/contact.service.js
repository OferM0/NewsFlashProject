import axios from "axios";
import { startEndpoint } from "./api";

//add contact details
export const addContactDetails = async (details) => {
  try {
    let endpoint = startEndpoint + "/Contacts/Add";
    await axios.post(endpoint, details);
  } catch (e) {
    console.log(e);
  }
};
