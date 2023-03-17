import React, { useEffect, useState } from "react";
import "./news.css";
import {
  getNewsItems,
  getNewsItemsByTopic,
  updateNewsItemById,
  getNewsItemById,
  getTrendingNewsItems,
  getCuriousNewsItems,
} from "../../../services/newsItem.service";
import { getWebsites } from "../../../services/website.service";
import { NewsTicker } from "../../../components/newsTicker/NewsTicker";
import { useAuth0 } from "@auth0/auth0-react";

export const NewsPage = ({ topic }) => {
  const { user } = useAuth0();
  const [websites, setWebsites] = useState([]);
  const [newsItems, setNewsItems] = useState([]);
  const [newsTitles, setNewsTitles] = useState([]);

  const fetchWebsites = async () => {
    let response = await getWebsites();
    if (response.status === 200) {
      const names = response.data.map((website) => website.name);
      setWebsites(names);
      console.log(websites);
    }
  };

  const fetchData = async () => {
    if (topic === "Trending") {
      let response = await getTrendingNewsItems(user.sub);
      if (response.status === 200) {
        setNewsItems(response.data);
        console.log(newsItems);
        return Promise.resolve(); // return a resolved promise
      }
    } else if (topic === "Curious") {
      let response = await getCuriousNewsItems(user.sub);
      if (response.status === 200) {
        setNewsItems(response.data);
        console.log(newsItems);
        return Promise.resolve(); // return a resolved promise
      }
    } else {
      let response = await getNewsItemsByTopic(topic);
      if (response.status === 200) {
        setNewsItems(response.data);
        console.log(newsItems);
        return Promise.resolve(); // return a resolved promise
      }
    }
  };

  const fetchData2 = async () => {
    let response = await getNewsItemsByTopic("BreakingNews");
    if (response.status === 200) {
      const titles = response.data.map(
        (breakingNewsItem) => breakingNewsItem.title
      );
      setNewsTitles(titles);
      console.log(titles);
    }
  };

  const clickOnNewsItem = async (newsItemId, clickCount) => {
    await updateNewsItemById(newsItemId, clickCount);
  };

  useEffect(() => {
    fetchWebsites();
    fetchData().then(() => setTimeout(fetchData2, 5000));
  }, [topic]);

  return (
    <div class="newsPage">
      <NewsTicker headlines={newsTitles} />
      {newsItems.length > 0 ? (
        newsItems.map((item) => (
          <div class="item" key={item.itemId}>
            <div class="item__header">
              <img
                src={item.imageUrl}
                alt={item.title}
                width="600"
                class="item__image"
              />
            </div>
            <div class="item__body">
              <span class="tag tag-red">{topic}</span>
              <h4>{item.title}</h4>
              <p>{item.description}</p>
            </div>
            <div class="item__footer">
              <div class="user">
                <div class="user__info">
                  <h5>{websites[item.webSiteId - 1]}</h5>
                  <small data-date={item.publishDate.replace("T", " ")}></small>
                  <a
                    href={item.link}
                    target="_blank"
                    rel="noopener noreferrer"
                    className="read-btn-container"
                  >
                    <button
                      className="read-btn"
                      onClick={() => {
                        console.log(item.itemId, item.clickCount);
                        let newClickCount = item.clickCount + 1;
                        console.log(item.itemId, newClickCount);
                        clickOnNewsItem(item.itemId, newClickCount);
                      }}
                    >
                      Read
                    </button>
                  </a>
                </div>
              </div>
            </div>
          </div>
        ))
      ) : (
        <h1 style={{ marginTop: "100px", marginLeft: "400px" }}>
          {"Don't have any " + topic + " news"}
        </h1>
      )}
    </div>
  );
};
