import React from 'react';
import Header from "../Components/Header/Header";
import CourseCard from "../Components/CourseCard/CourseCard";

import "./Courses.css"
import CourseAddButton from "../Components/CourseCard/AddCourseButton/CourseAddButton";

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
            <CourseAddButton/>
        </>
    );
};

export default Courses;