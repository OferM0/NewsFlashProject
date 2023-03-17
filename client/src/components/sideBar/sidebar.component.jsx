import React from "react";
import "./style.css";
import HomeIcon from "@mui/icons-material/Home";
import InfoIcon from "@mui/icons-material/Info";
import CallIcon from "@mui/icons-material/Call";
import ProductIcon from "@mui/icons-material/Store";
import FaceIconfrom from "@mui/icons-material/Face";
import LogOutIconfrom from "@mui/icons-material/Logout";
import RegisterIcon from "@mui/icons-material/AppRegistration";
import "../../../node_modules/bootstrap/dist/css/bootstrap.css";
import { Link } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";

export const SideBar = (props) => {
  const { logout } = useAuth0();

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
            <Link to="/about" className="nav-link link-dark">
              <InfoIcon />
              <span className="section-name">About</span>
            </Link>
          </li>
          <li>
            <Link to="/contacts" className="nav-link link-dark">
              <CallIcon />
              <span className="section-name">Contact Us</span>
            </Link>
          </li>
          <li>
            <Link to="/products" className="nav-link link-dark">
              <ProductIcon />
              <span className="section-name">Products</span>
            </Link>
          </li>
          <li>
            <Link to="/register" className="nav-link link-dark">
              <RegisterIcon />
              <span className="section-name">register</span>
            </Link>
          </li>
          <li>
            <Link to="/profile" className="my-name nav-link link-dark">
              <FaceIconfrom />
              <span className="section-name">Profile</span>
            </Link>
          </li>
          <li>
            <a
              href="#"
              className="nav-link link-dark"
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
