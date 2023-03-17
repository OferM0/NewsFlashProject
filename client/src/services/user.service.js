import axios from "axios";
import { startEndpoint } from "./api";

//add user details
export const addUserDetails = async (details) => {
  try {
    let endpoint = startEndpoint + "/users/add";
    await axios.post(endpoint, details);
  } catch (e) {
    console.log(e);
  }
};

//get Users
export const getUsers = async () => {
  try {
    let endpoint = startEndpoint + "/users/get";
    let response = await axios.get(endpoint);
    return response;
  } catch (e) {
    console.log(e);
  }
};

//get User by id
export const getUserById = async (UserID) => {
  try {
    let endpoint = startEndpoint + `/users/get/${UserID}`;
    let response = await axios.get(endpoint);
    return response;
  } catch (e) {
    console.log(e);
  }
};

//update User
export const updateUserById = async (UserID, User) => {
  try {
    let endpoint = `${startEndpoint}/users/update/${UserID}`;
    console.log("Updating user:", UserID, User, endpoint);
    await axios.put(endpoint, User);
    console.log("User updated successfully");
  } catch (e) {
    console.log("Error updating user:", e);
  }
};

export const updateUserInterestsById = async (UserID, Interests) => {
  try {
    let endpoint = `${startEndpoint}/users/updateInterests/${UserID}`;
    console.log("Updating user's interests:", UserID, Interests, endpoint);
    await axios.put(endpoint, Interests);
    console.log("User's interests updated successfully");
  } catch (e) {
    console.log("Error updating user's interests:", e);
  }
};

//delete User
export const removeUserById = async (UserID) => {
  try {
    let endpoint = startEndpoint + `/users/remove/${UserID}`;
    await axios.delete(endpoint);
  } catch (e) {
    console.log(e);
  }
};
