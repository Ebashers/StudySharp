import React from 'react';
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faUserCircle} from "@fortawesome/free-regular-svg-icons";
import "./Profile.css"


const Profile = () => {
    return (
        <div className="profile">
            <FontAwesomeIcon className="profile__icon" style={{width:"32px"}} icon={faUserCircle}/>
        </div>
    );
};

export default Profile;