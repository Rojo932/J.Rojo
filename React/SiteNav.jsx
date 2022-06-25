import React from 'react'
import {useNavigate} from "react-router-dom"

function Navigate(props) {

    const navigate = useNavigate();
    const goToPage = e =>
        {
            console.log(e.currentTarget.dataset.page)
            navigate(e.currentTarget.dataset.page)
        }
  return (
    <React.Fragment>
    <nav
      className="navbar navbar-expand-md navbar-dark bg-dark"
      aria-label="Fourth navbar example"
     >
      <div className="container" >
        <div className="navbar-brand">
          <img
            src="https://pw.sabio.la/images/Sabio.png"
            width="30"
            height="30"
            className="d-inline-block align-top"
            alt="Sabio"
            onClick={goToPage} 
            data-page="/"
          />
        </div>
        <button
          className="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarsExample04"
          aria-controls="navbarsExample04"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>

        <div className="collapse navbar-collapse" id="navbarsExample04">
          <ul className="navbar-nav me-auto mb-2 mb-md-0">
            <li className="nav-item">
              <button onClick={goToPage} data-page="/" id="pgHome" className="nav-link px-2 text-white link-button">
                Home
              </button>
            </li>
            <li className="nav-item">
              <button onClick={goToPage} data-page="/friends" id="pgFriends" className="nav-link px-2 text-white link-button">
                Friends
              </button>
            </li>
            <li className="nav-item">
              <button onClick={goToPage} data-page="/jobs" id="pgJobs" className="nav-link px-2 text-white link-button">
                Jobs
              </button>
            </li>
            <li className="nav-item">
              <button onClick={goToPage} data-page="/tech-companies" id="pgTechCompanies" className="nav-link px-2 text-white link-button">
                Tech Companies
              </button>
            </li>
            <li className="nav-item">
              <button onClick={goToPage} data-page="/events" id="pgEvents" className="nav-link px-2 text-white link-button">
                Events
              </button>
            </li>
            <li className="nav-item">
              <button onClick={goToPage} data-page="/test-and-ajax-call" id="pgTestAndAjaxCall" className="nav-link px-2 text-white link-button"
              >
                Test and Ajax Call
              </button>
            </li>
            <li className="nav-item">
              <button onClick={goToPage} data-page="/product" id="product" className="nav-link px-2 text-white link-button"
              >
                Product
              </button>
            </li>
            <li className="nav-item">
              <button onClick={goToPage} data-page="/car" id="car" className="nav-link px-2 text-white link-button"
              >
                Car
              </button>
            </li>
          </ul>
          <div className="text-end">
            <a
              href="/"
              className="align-items-center mb-2 me-2 mb-lg-0 text-white text-decoration-none"
            >
              {props.user.firstName} {props.user.lastName}
            </a>
            <button onClick={goToPage} data-page="/login" id="pgLogin" type="button" className="btn btn-outline-light me-2">
              Login
            </button>
            <button onClick={goToPage} data-page="/register" id="pgRegister" type="button" className="btn btn-warning">
              Register
            </button>
          </div>
        </div>
      </div>
    </nav>
    </React.Fragment>
  )
}

export default Navigate
