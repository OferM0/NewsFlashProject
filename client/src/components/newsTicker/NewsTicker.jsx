import React from "react";
import "./NewsTicker.css";
import PublicIcon from "@mui/icons-material/Public";
import logo from "../../images/logo3.jpg";

export const NewsTicker = ({ headlines }) => {
  return (
    <div className="news-ticker-container">
      <img
        src={logo}
        alt="Logo"
        style={{
          width: "120px",
          height: "40px",
          position: "absolute",
          right: "0",
          zIndex: "1",
        }}
      />
      <div className="news-ticker-items">
        {headlines.map((headline) => (
          <div key={headline} className="news-ticker-item">
            <div className="newText">{headline}</div>
            <div className="logo-world">
              <PublicIcon
                fontSize="small"
                style={{ color: " rgb(175, 55, 55)" }}
              />
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};
