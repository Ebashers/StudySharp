import React from 'react';
import Header from "../Components/Header/Header";
import CourseCard from "../Components/CourseCard/CourseCard";

import "./Courses.css"

const Courses = () => {
    const arr = [
        <CourseCard/>,
        <CourseCard/>,
        <CourseCard/>,
    ]

    return (
        <>
            <Header/>
            <div className="cards">
                {arr.map(el => el)}
            </div>
            
        </>
    );
};

export default Courses;