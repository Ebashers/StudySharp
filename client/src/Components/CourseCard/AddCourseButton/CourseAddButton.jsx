import React from 'react';
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faPlusCircle} from "@fortawesome/free-solid-svg-icons"

import "./CourseAddButton.css"


const CourseAddButton = () => {
    return (
        <a href="#" className="course-add-button">
            <FontAwesomeIcon className="sign" icon={faPlusCircle}/>
            create new course
        </a>
    );
};

export default CourseAddButton;