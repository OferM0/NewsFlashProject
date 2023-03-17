import React, { useContext, useState, useEffect } from "react";
import "./style.css";
import { UserDetailsContext } from "../../context/userDetails.context";
import { Link } from "react-router-dom";
import { NewsTicker } from "../../components/newsTicker/NewsTicker";
import { getNewsItemsByTopic } from "../../services/newsItem.service";

export const HomePage = (props) => {
  const { userDetails } = useContext(UserDetailsContext);
  const [newsTitles, setNewsTitles] = useState([]);

  const fetchData = async () => {
    let response = await getNewsItemsByTopic("BreakingNews");
    if (response.status === 200) {
      const titles = response.data.map((newsItem) => newsItem.title);
      setNewsTitles(titles);
      console.log(titles);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <div className="home">
      <NewsTicker headlines={newsTitles} />
      <div className="information">
        <h3 className="title">Flash News</h3>
        <h1>Get your favourites news anytime</h1>
        <p className="home-text"></p>
        {userDetails === null ? (
          <>
            <Link to="/activist/campaigns">
              <button className="buy-now">START NOW</button>
            </Link>
          </>
        ) : (
          <>
            <Link to="/register">
              <button className="buy-now">START NOW</button>
            </Link>
          </>
        )}
      </div>
    </div>
  );
};
