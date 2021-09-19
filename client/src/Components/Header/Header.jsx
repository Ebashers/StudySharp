import React from 'react';
import Logo from "../HeaderComponents/Logo/Logo";
import Navbar from "../HeaderComponents/Navbar/Navbar";
import Profile from "../HeaderComponents/Profile/Profile";

import "./Header.css"

const Header = () => {
    const array = ["My courses", "Statistics", "My organization"]

    return (
        <header className="header">
            <div className="wrapper">
                <Logo/>
                <Navbar array={array} />
                <Profile/>
            </div>
        </header>
    );
};

export default Header;