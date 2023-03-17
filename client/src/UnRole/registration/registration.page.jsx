import React, { useState, useEffect } from "react";
import "./registration.css";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { useAuth0 } from "@auth0/auth0-react";
import { addUserDetails } from "../../services/user.service";

const showToastMessage = () => {
  toast.success("We recived your registration, Please Login again!", {
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

export const RegistrationPage = () => {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [phone, setPhone] = useState("");
  const { user, logout } = useAuth0();
  const sleep = (ms) => {
    return new Promise((resolve) => setTimeout(resolve, ms));
  };

  const handleSubmit = async () => {
    // let date = new Date();

    let details = {
      id: user.sub,
      name: name,
      email: email,
      phoneNumber: phone,
      // CreateDate: date.toISOString().slice(0, 10),
    };
    await addUserDetails(details);
    setName("");
    setEmail("");
    setPhone("");
  };

  useEffect(() => {}, []);

  return (
    <div className="registerPage">
      <div className="container register">
        <div className="row">
          <div className="col-md-3 register-left">
            <h3>Welcome</h3>
            <p>
              Please sign up to start getting your favorite news.
              <br />
            </p>
          </div>

          <div className="col-md-9 register-right">
            <div className="tab-content" id="myTabContent">
              <div
                className="tab-pane fade show active"
                id="home"
                role="tabpanel"
                aria-labelledby="home-tab"
              >
                <div className="row register-form">
                  <div style={{ marginTop: "-80px" }} className="col-md-6">
                    <input
                      type="text"
                      className="form-control"
                      placeholder="Full Name"
                      maxLength="30"
                      onChange={(e) => {
                        setName(e.target.value);
                      }}
                      value={name}
                    />
                    <input
                      type="text"
                      className="form-control"
                      placeholder="Email"
                      maxLength="40"
                      onChange={(e) => {
                        setEmail(e.target.value);
                      }}
                      value={email}
                    />
                    <input
                      type="text"
                      minLength="10"
                      maxLength="10"
                      className="form-control"
                      placeholder="Phone"
                      onChange={(e) => {
                        setPhone(e.target.value);
                      }}
                      value={phone}
                    />
                  </div>
                  <button
                    className="btn-Register"
                    onClick={async () => {
                      if (
                        email.length < 6 ||
                        /^0\d{9}$/.test(phone) === false ||
                        isValidFullName(name) === false ||
                        name == "" ||
                        email == "" ||
                        phone == ""
                      ) {
                        showWarningMessage();
                      } else {
                        handleSubmit();
                        showToastMessage();
                        await sleep(2000);
                        logout({ returnTo: window.location.origin });
                      }
                    }}
                  >
                    Register
                  </button>
                  <ToastContainer />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};
