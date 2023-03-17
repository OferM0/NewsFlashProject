import axios from "axios";
import { startEndpoint } from "./api";

//add newsitem details
export const addNewsItem = async (details) => {
  try {
    let endpoint = startEndpoint + "/newsitems/add";
    await axios.post(endpoint, details);
  } catch (e) {
    console.log(e);
  }
};

//get NewsItems
export const getNewsItems = async () => {
  try {
    let endpoint = startEndpoint + "/newsitems/get";
    let response = await axios.get(endpoint);
    return response;
  } catch (e) {
    console.log(e);
  }
};

export const getNewsItemsByTopic = async (topic) => {
  try {
    let endpoint = startEndpoint + `/newsitems/getbytopic?topic=${topic}`;
    let response = await axios.get(endpoint);
    return response;
  } catch (e) {
    console.log(e);
  }
};

export const getTrendingNewsItems = async (userId) => {
  try {
    let endpoint = startEndpoint + `/newsitems/gettrending?userId=${userId}`;
    let response = await axios.get(endpoint);
    return response;
  } catch (e) {
    console.log(e);
  }
};

export const getCuriousNewsItems = async (userId) => {
  try {
    let endpoint = startEndpoint + `/newsitems/getcurious?userId=${userId}`;
    let response = await axios.get(endpoint);
    return response;
  } catch (e) {
    console.log(e);
  }
};

//get NewsItem by id
export const getNewsItemById = async (NewsItemID) => {
  try {
    let endpoint = startEndpoint + `/newsitems/get/${NewsItemID}`;
    let response = await axios.get(endpoint);
    return response;
  } catch (e) {
    console.log(e);
  }
};

//update NewsItem
export const updateNewsItemById = async (NewsItemID, clickCount) => {
  try {
    let endpoint = `${startEndpoint}/newsitems/update/${NewsItemID}?ClickCount=${clickCount}`;
    console.log("Updating newsitem:", NewsItemID, clickCount, endpoint);
    await axios.put(endpoint);
    console.log("NewsItem updated successfully");
  } catch (e) {
    console.log("Error updating newsitem:", e);
  }
};

//delete NewsItem
export const removeNewsItemById = async (NewsItemID) => {
  try {
    let endpoint = startEndpoint + `/newsitems/remove/${NewsItemID}`;
    await axios.delete(endpoint);
  } catch (e) {
    console.log(e);
  }
};
