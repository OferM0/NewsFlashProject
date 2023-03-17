import React, { useState, useContext } from "react";
import "./style.css";
import "../../../node_modules/bootstrap/dist/css/bootstrap.css";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { addContactDetails } from "../../services/contact.service";
import { UserDetailsContext } from "../../context/userDetails.context";
import { useAuth0 } from "@auth0/auth0-react";

const showToastMessage = () => {
  toast.success("We recived your message!", {
    position: "top-right",
    autoClose: 3000,
    hideProgressBar: false,
    closeOnClick: true,
    pauseOnHover: true,
    draggable: true,
    progress: undefined,
    theme: "light",
  });
};

const showWarningMessage = () => {
  toast.error("Please check all fields are valid!", {
    position: "top-right",
    autoClose: 3000,
    hideProgressBar: false,
    closeOnClick: true,
    pauseOnHover: true,
    draggable: true,
    progress: undefined,
    theme: "light",
  });
};

function isValidFullName(name) {
  let nameRegex = /^[a-zA-Z]+\s[a-zA-Z]+$/;
  if (nameRegex.test(name) === false) {
    return false;
  }

  if (name.length < 2) {
    return false;
  }

  let nameArray = name.split(" ");
  if (nameArray.length < 2) {
    return false;
  }
  if (nameArray[0].length < 2 || nameArray[1].length < 2) {
    return false;
  }
  return true;
}

function validateEmail(email) {
  if (email.length < 3) {
    return false;
  }
  var re =
    /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z0-9\-_+.]+\.)+[a-zA-Z]{2,}))$/;
  return re.test(String(email).toLowerCase());
}

export const ContactsPage = (props) => {
  const { userDetails } = useContext(UserDetailsContext);
  const { user } = useAuth0();
  const [name, setName] = useState(userDetails.Name || "");
  const [email, setEmail] = useState(user.email);
  const [phone, setPhone] = useState(userDetails.Phone || "");
  const [message, setMessage] = useState("");

  const handleSubmit = async () => {
    let details = { Name: name, Email: email, Phone: phone, Message: message };
    await addContactDetails(details);
    setName(userDetails.Name || "");
    setEmail(user.Email);
    setPhone(userDetails.Phone || "");
    setMessage("");
  };

  return (
    <div className="contacts">
      <div className=" information2">
        <h3 className="text-center title2">Get in touch with us</h3>
        <div className="row">
          <div className="col-md-12 field">
            <div className="form-group">
              <input
                type="text"
                className="form-control form-control-contact"
                name="name"
                maxLength="30"
                id="name"
                placeholder="Name"
                onChange={(e) => {
                  setName(e.target.value);
                }}
                value={name}
              />
            </div>
          </div>
          <div className="col-md-12 field">
            <div className="form-group">
              <input
                type="email"
                className="form-control form-control-contact"
                name="email"
                id="email"
                maxLength="50"
                placeholder="Email"
                onChange={(e) => {
                  setEmail(e.target.value);
                }}
                value={email}
              />
            </div>
          </div>
          <div className="col-md-12 field">
            <div className="form-group">
              <input
                type="text"
                className="form-control form-control-contact"
                name="phone"
                id="phone"
                maxLength="10"
                placeholder="Phone"
                onChange={(e) => {
                  setPhone(e.target.value);
                }}
                value={phone}
              />
            </div>
          </div>
          <div className="col-md-12 field">
            <div className="form-group">
              <textarea
                name="message"
                className="form-control form-control-contact"
                id="message"
                maxLength="300"
                cols="30"
                rows="8"
                placeholder="Message"
                onChange={(e) => {
                  setMessage(e.target.value);
                }}
                value={message}
              ></textarea>
            </div>
          </div>
          <button
            className="sendbtn"
            onClick={() => {
              if (
                name == "" ||
                email == "" ||
                phone == "" ||
                message == "" ||
                /^0\d{9}$/.test(phone) === false ||
                isValidFullName(name) === false ||
                message.length < 6 ||
                validateEmail(email) === false
              ) {
                showWarningMessage();
              } else {
                handleSubmit();
                showToastMessage();
              }
            }}
          >
            Send Message
          </button>
          <ToastContainer />
        </div>
      </div>
    </div>
  );
};
