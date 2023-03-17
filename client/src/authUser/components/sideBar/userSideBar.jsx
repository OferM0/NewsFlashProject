import React, { useContext, useEffect } from "react";
import "./userSideBar.css";
import HomeIcon from "@mui/icons-material/Home";
import InfoIcon from "@mui/icons-material/Info";
import CallIcon from "@mui/icons-material/Call";
import SettingsIcon from "@mui/icons-material/Settings";
import FaceIconfrom from "@mui/icons-material/Face";
import LogOutIconfrom from "@mui/icons-material/Logout";
import TrendIcon from "@mui/icons-material/TrendingUp";
import CuriousIcon from "@mui/icons-material/PsychologyAlt";
import SportsIcon from "@mui/icons-material/Sports";
import FoodIcon from "@mui/icons-material/FoodBank";
import HealthIcon from "@mui/icons-material/HealthAndSafety";
import AutomotiveIcon from "@mui/icons-material/CarCrash";
import EconomyIcon from "@mui/icons-material/Money";
import JudaismIcon from "@mui/icons-material/Synagogue";
import TourismIcon from "@mui/icons-material/Tour";
import CultureIcon from "@mui/icons-material/Museum";
import ConsumerismIcon from "@mui/icons-material/ShoppingCart";
import OpinionsIcon from "@mui/icons-material/Lightbulb";
import EnvironmentIcon from "@mui/icons-material/Nature";
import TechnologyIcon from "@mui/icons-material/Computer";
import { Link } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";
import { getUserById } from "../../../services/user.service";
import { UserDetailsContext } from "../../../context/userDetails.context";

export const UserSideBar = (props) => {
  const { logout } = useAuth0();
  const { userDetails, setUserDetails, interestsSaved, setInterestsSaved } =
    useContext(UserDetailsContext);

  const interestIcons = {
    sports: SportsIcon,
    food: FoodIcon,
    health: HealthIcon,
    automotive: AutomotiveIcon,
    economy: EconomyIcon,
    judaism: JudaismIcon,
    tourism: TourismIcon,
    culture: CultureIcon,
    consumerism: ConsumerismIcon,
    opinions: OpinionsIcon,
    environment: EnvironmentIcon,
    technology: TechnologyIcon,
  };

  const fetchData = async () => {
    if (interestsSaved) {
      let response = await getUserById(userDetails.userId);
      console.log(response);
      if (response.status === 200) {
        setUserDetails(response.data);
        setInterestsSaved(false);
        console.log(userDetails);
      }
    }
  };

  useEffect(() => {
    fetchData();
  }, [userDetails, interestsSaved]);

  const interestLinks = userDetails.interests
    .filter((interest) => interest !== "BreakingNews")
    .map((interest) => {
      const path = `/${interest.toLowerCase()}News`;
      const InterestIcon = interestIcons[interest.toLowerCase()];
      return (
        <li key={interest}>
          <Link to={path} className="nav-link link-dark aa">
            <InterestIcon />
            <span className="section-name">{interest}</span>
          </Link>
        </li>
      );
    });

  return (
    <>
      <div className="container2 p-3">
        <ul className="nav nav-pills flex-column">
          <li className="nav-item">
            <Link to="/" className="nav-link link-dark aa">
              <HomeIcon />
              <span className="section-name">Home</span>
            </Link>
          </li>
          <li>
            <Link to="/about" className="nav-link link-dark aa">
              <InfoIcon />
              <span className="section-name">About</span>
            </Link>
          </li>
          <li>
            <Link to="/contacts" className="nav-link link-dark aa">
              <CallIcon />
              <span className="section-name">Contact Us</span>
            </Link>
          </li>
          <li>
            <Link to="/trendingNews" className="nav-link link-dark aa">
              <TrendIcon />
              <span className="section-name">Trending</span>
            </Link>
          </li>
          <li>
            <Link to="/curiousNews" className="nav-link link-dark aa">
              <CuriousIcon />
              <span className="section-name">Curious</span>
            </Link>
          </li>
          {interestLinks}
          <li>
            <Link to="/settings" className="nav-link link-dark aa">
              <SettingsIcon />
              <span className="section-name">Settings</span>
            </Link>
          </li>
          <li>
            <Link to="/userProfile" className="my-name nav-link link-dark aa">
              <FaceIconfrom />
              <span className="section-name">Profile</span>
            </Link>
          </li>
          <li>
            <a
              href="#"
              className="nav-link link-dark aa"
              onClick={() => logout({ returnTo: window.location.origin })}
            >
              <LogOutIconfrom />
              <span className="section-name">Log Out</span>
            </a>
          </li>
        </ul>
      </div>
    </>
  );
};
