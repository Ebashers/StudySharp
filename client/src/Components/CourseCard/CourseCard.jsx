import React from 'react';
import "./CourseCard.css"
import CourseCardInfo from "./CourseCardComponents/CourseCardInfo/CourseCardInfo";

const CourseCard = () => {
    return (
        <div className="card">
            <div className="img">

            </div>
            <CourseCardInfo/>
        </div>
    );
};

export default CourseCard;