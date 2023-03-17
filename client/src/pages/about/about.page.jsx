import React from "react";
import "./style.css";

export const AboutPage = (props) => {
  return (
    <div className="about">
      <div>
        <p className="text">
          ProLobby Company seeking to build a new system - "PromoIt," that
          handles the social promotion of the non-profit organization and other
          related campaigns.
          <br /> For the first version, we only use the Twitter social network.
          The system's goal is to promote social campaigns.
          <br />
          The means to do so involve onboarding business organizations that
          donate products, onboarding non-profit organizations that want to
          promote campaigns, and onboarding social activists - users of Twitter
          that can promote those campaigns.
        </p>
      </div>
      <div class="contact-info">
        <h4>General Inquiries</h4>
        <p className="ques">Have a question… or just want to say hello?</p>
        <p className="ques">
          <a
            class="gold-text-button"
            href="mailto:ofermordehai0@gmail.com"
            target="_blank"
            rel="noopener"
          >
            ofermordehai0@gmail.com
          </a>
          <a class="gold-text-button" target="_blank" rel="noopener">
            0528016257
          </a>
        </p>
        <h4>Our Location</h4>
        <p className="ques">Moshe Sharet 6</p>
        <p className="ques">Israel, Akko</p>
        <p className="ques">
          <a
            class="gold-text-button"
            href="https://goo.gl/maps/4vMTgzd5zE98AqrTA"
            target="_blank"
            rel="noopener"
          >
            GET DIRECTIONS
          </a>
        </p>
      </div>
    </div>
  );
};
