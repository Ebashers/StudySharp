import React from 'react'
import {FontAwesomeIcon} from '@fortawesome/react-fontawesome'
import {faFacebookF, faGooglePlusG, faTelegramPlane} from '@fortawesome/free-brands-svg-icons'

function LogInLeft() {
    return (
        <div className='main-1'>
            <div className='title'>
                <h1>Log In</h1>
            </div>
            <div className='icons icon-1'>
                <div className='icon'>
                    <FontAwesomeIcon icon={faFacebookF}/>
                </div>
                <div className='icon icon-2'>
                    <FontAwesomeIcon icon={faGooglePlusG}/>
                </div>
                <div className='icon icon-3'>
                    <FontAwesomeIcon icon={faTelegramPlane}/>
                </div>
                <div className='text-1'>
                    <p>or use your account</p>
                </div>
                <div className='form'>
                    <div className='email'>
                        <form action="#">
                            <input type="text" placeholder="Email"></input>
                        </form>
                    </div>
                    <div className='password'>
                        <input type="text" placeholder="Password"></input>
                    </div>
                </div>
                <div className='text-2'>
                    <p>Forgot your password?</p>
                </div>
                <div className='button-1'>
                    <button>LOG IN</button>
                </div>
            </div>
        </div>
    )
}

export default LogInLeft