import "./App.css";
import React, { useState, useEffect } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import AuthUser from "./authUser/User";
import UnAuthUser from "./pages/unAuthUser/unAuthUser.page";
import { UserDetailsContext } from "./context/userDetails.context";
import { getUserById } from "./services/user.service";
import UnRole from "./UnRole/UnRole";
let check = 1;

function App() {
  const { isAuthenticated, isLoading, user } = useAuth0();
  const [userDetails, setUserDetails] = useState([]);
  const [interestsSaved, setInterestsSaved] = useState(false);
  const fetchData = async () => {
    let response = await getUserById(user.sub);
    console.log(response);
    if (response.status === 200) {
      setUserDetails(response.data);
      setInterestsSaved(false);
      console.log(userDetails);
    }
  };

  // useEffect(() => {
  //   fetchData();
  // }, []);

  if (!isLoading) {
    if (isAuthenticated) {
      if (check === 1) {
        fetchData();
        check++;
      }
      return (
        <UserDetailsContext.Provider
          value={{
            userDetails,
            setUserDetails,
            interestsSaved,
            setInterestsSaved,
          }}
        >
          <>
            {userDetails.userId == user.sub ? (
              <>
                <AuthUser />
              </>
            ) : (
              <>
                <UnRole />
              </>
            )}
          </>
        </UserDetailsContext.Provider>
      );
    } else {
      return <UnAuthUser />;
    }
  } else {
    <h1>loading</h1>;
  }
}

export default App;
