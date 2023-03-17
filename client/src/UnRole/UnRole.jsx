import "./unRole.css";
import { AboutPage, ContactsPage, HomePage, NotFoundPage } from "../pages";
import { Route, Routes } from "react-router-dom";
import React from "react";
import { UnRoleSideBar } from "./unRoleSideBar";
import { RegistrationPage } from "./registration/registration.page";
function UnRole() {
  return (
    <div className="UnRoleBoard">
      <UnRoleSideBar />
      <Routes>
        <Route path="/" element={<HomePage />}></Route>
        <Route path="/about" element={<AboutPage />}></Route>
        <Route path="/contacts" element={<ContactsPage />}></Route>
        <Route path="/register" element={<RegistrationPage />}></Route>
        <Route path="*" element={<NotFoundPage />}></Route>
      </Routes>
    </div>
  );
}

export default UnRole;
