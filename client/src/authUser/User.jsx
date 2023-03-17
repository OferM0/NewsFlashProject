import "./User.css";
import { HomePage, AboutPage, ContactsPage, NotFoundPage } from "../pages";
import { Route, Routes } from "react-router-dom";
import React, { useEffect, useContext, useState } from "react";
import { UserSideBar } from "./components/sideBar/userSideBar";
import { SettingsPage } from "./pages/settings/settings";
import { UserProfileEditPage } from "./pages/profile/userProfileEdit";
import { UserProfilePage } from "./pages/profile/userProfile";
import { getUserById } from "../services/user.service";
import { UserDetailsContext } from "../context/userDetails.context";
import { NewsPage } from "./pages/news/news";
import { getCategories } from "../services/category.service";

// const newsSubjects = [
//   "BreakingNews",
//   "Sports",
//   "Food",
//   "Health",
//   "Automotive",
//   "Economy",
//   "Judaism",
//   "Tourism",
//   "Culture",
//   "Consumerism",
//   "Opinions",
//   "Environment",
//   "Technology",
//   "Celebrities",
//   "House & Design",
// ];

function AuthUser() {
  const { userDetails, setUserDetails } = useContext(UserDetailsContext);
  const [categories, setCategories] = useState([]);

  const fetchCategoriess = async () => {
    let response = await getCategories();
    if (response.status === 200) {
      const topics = response.data
        .filter((category) => category.topic !== "BreakingNews")
        .map((category) => category.topic);
      setCategories(topics);
      console.log(categories);
    }
  };

  // Create an array of routes based on the user's interests
  const interestRoutes = userDetails.interests.map((interest) => {
    // Convert the interest to a route path
    const path = `/${interest.toLowerCase()}News`;
    // Create a new Route element for this interest
    return (
      <Route
        key={interest}
        path={path}
        element={<NewsPage topic={interest} />}
      />
    );
  });

  useEffect(() => {
    fetchCategoriess();
  }, [categories]);

  return (
    <div className="UserDashBoard">
      <UserSideBar />
      <Routes>
        <Route path="/" element={<HomePage />}></Route>
        <Route path="/about" element={<AboutPage />}></Route>
        <Route path="/contacts" element={<ContactsPage />}></Route>
        <Route
          path="/trendingNews"
          element={<NewsPage topic={"Trending"} />}
        ></Route>
        <Route
          path="/curiousNews"
          element={<NewsPage topic={"Curious"} />}
        ></Route>
        {/* Add the interest-specific routes */}
        {interestRoutes}
        <Route
          path="/settings"
          element={<SettingsPage subjects={categories} />}
        ></Route>
        <Route path="/userProfile" element={<UserProfilePage />}></Route>
        <Route
          path="/userProfile/edit"
          element={<UserProfileEditPage />}
        ></Route>
        <Route path="*" element={<NotFoundPage />}></Route>
      </Routes>
    </div>
  );
}

export default AuthUser;
