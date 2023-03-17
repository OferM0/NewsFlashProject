import React, { useState, useContext, useEffect } from "react";
import "./settings.css";
import AddIcon from "@mui/icons-material/Add";
import VIcon from "@mui/icons-material/Done";
import { UserDetailsContext } from "../../../context/userDetails.context";
import {
  getUserById,
  updateUserInterestsById,
} from "../../../services/user.service";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

const showToastMessage = () => {
  toast.success("Your settings updated successfully!", {
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

export const SettingsPage = ({ subjects }) => {
  //const [userDetails, setUserDetails] = useState([]);
  const { userDetails, setUserDetails, setInterestsSaved } =
    useContext(UserDetailsContext);
  //const [checked, setChecked] = useState(subjects.map(() => false));
  const [checked, setChecked] = useState(
    subjects.map((subject) => userDetails.interests.includes(subject))
  );

  const fetchData = async () => {
    let response = await getUserById(userDetails.userId);
    if (response.status === 200) {
      setUserDetails(response.data);
    }
  };

  // useEffect(() => {
  //    fetchData();
  // }, []);

  const handleCheck = (index) => {
    // Count the number of selected chips
    const numSelected = checked.filter((c) => c).length;

    // Allow up to 3 selected chips
    if (numSelected < 3 || checked[index]) {
      const newChecked = [...checked];
      newChecked[index] = !newChecked[index];
      setChecked(newChecked);
    }
  };

  const handleSubmit = async () => {
    console.log(userDetails);
    let copyUserDetails = userDetails;
    copyUserDetails.interests = subjects.filter(
      (subject, index) => checked[index]
    );
    copyUserDetails.interests = [...copyUserDetails.interests, "BreakingNews"];
    console.log(copyUserDetails);
    await updateUserInterestsById(
      userDetails.userId,
      copyUserDetails.interests
    );
    setUserDetails(copyUserDetails);
    setInterestsSaved(true);
  };

  return (
    <div className="settings-container">
      <div className="settings-info">
        <h1>What are you into?</h1>
        <h3>Follow tags & topics you want to see. Follow up to 3</h3>
      </div>
      <div className="chipsContainer">
        {subjects.map((subject, index) => (
          <button
            key={index}
            className="chip"
            style={
              checked[index]
                ? { color: "#1e5492", backgroundColor: "white" }
                : {}
            }
            onClick={() => {
              handleCheck(index);
            }}
          >
            <span className="chipContent">
              <div className="chipIcon">
                {checked[index] ? <VIcon /> : <AddIcon />}
              </div>
              {subject}
            </span>
          </button>
        ))}
      </div>
      <button
        className="save-settings"
        onClick={() => {
          console.log(checked);
          handleSubmit();
          showToastMessage();
        }}
      >
        Save Your Settings
      </button>
      <ToastContainer />
    </div>
  );
};
