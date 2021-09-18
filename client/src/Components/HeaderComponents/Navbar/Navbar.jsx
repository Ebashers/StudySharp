import React from 'react';
import "./Navbar.css"

const Navbar = ({array}) => {
    return (
        <div className="nav">
            <ul className="navbar">
                {array.map(el => (
                    <li>
                        {/*eslint-disable-next-line jsx-a11y/anchor-is-valid */}
                        <a href="#">
                            {el}
                        </a>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default Navbar;