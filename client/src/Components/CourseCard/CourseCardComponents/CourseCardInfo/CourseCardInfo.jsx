import React from 'react';

import {FontAwesomeIcon} from '@fortawesome/react-fontawesome'

import {faStar, faUser, faSignal} from "@fortawesome/free-solid-svg-icons"

import "./CourseCardInfo.css"

const CourseCardInfo = () => {
    const arr = [1, 1, 1, 1, 1]

    return (
        <div className="course">
            <div className="course__name">Name of the course
                <div className="course__organization">Organization</div>
            </div>
            <div className="course__text">Lorem ipsum dolor sit amet, consectetur adipisicing elit. Aperiam
                assumenda consectetur dignissimos dolores eos est eum ipsa, libero nam officia pariatur possimus,
                praesentium repudiandae temporibus vero vitae voluptas voluptatem! Modi.
            </div>
            <div className="course__info">
                <div className="course__info-stars">
                    {arr.map((e) => <FontAwesomeIcon icon={faStar}/>)}
                    <b className="stars__mark">4.8(124)</b>
                </div>
                <div className="course__info-members">
                    <FontAwesomeIcon icon={faUser}/>
                    <b className="stars__mark">654</b>
                </div>
                <div className="course__info-level">
                    <FontAwesomeIcon icon={faSignal}/>
                    <b className="stars__mark">Beginner</b>
                </div>
            </div>
        </div>
    );
};

export default CourseCardInfo;