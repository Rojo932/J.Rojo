import React, { useState } from "react";
import userService from "../../services/userService";
import toastr from "toastr";


function Login() {
  const [user, setUser] = useState({
    email: "",
    password: "",
    tenantId: "rojo932",
  });

  var onloginSuccess = (response) => {
    console.log("onloginSuccess >>", response);
    toastr.success("Successfull Login");
  };

  var onloginFalure = (response) => {
    console.error(response);
    toastr.error("Failed Login");
  };

  const loginHandler = (event) => {
    event.preventDefault();
    console.log("loginHandler >>", user);
    userService.logIn(user).then(onloginSuccess).catch(onloginFalure);
  };



  const onFormFieldChange = (event) => {
    console.log("onChange", event.currentTarget);

    const {name, value} = event.currentTarget;
    console.log({ name, value });

    //Updater
    setUser((prevState) => {
      console.log("updater onChange");

      const newUserObject = {
        ...prevState,
      };

      newUserObject[name] = value;

      return newUserObject;
    });
    console.log("end onChange");
  };

  return (
    <React.Fragment>
      <section
        className="bg-image vh-100"
        style={{
          backgroundImage: `url("https://mdbcdn.b-cdn.net/img/Photos/new-templates/search-box/img4.webp")`,
        }}
      >
        <div className="mask d-flex align-items-center h-100 gradient-custom-3">
          <div className="container h-100">
            <div className="row d-flex justify-content-center align-items-center h-100">
              <div className="col-12 col-md-9 col-lg-7 col-xl-6">
                <div className="card">
                  <div className="card-body p-5 cardAp">
                    <form>
                      <h3 className="fw-normal mb-3 pb-3">Log in</h3>
                      <div className="form-outline mb-4">
                        <input
                          name="email"
                          type="email"
                          id="emailInput1"
                          className="form-control form-control-lg"
                          value={user.email}
                          onChange={onFormFieldChange}
                        />
                        <label className="form-label" htmlFor="email">
                          Email address
                        </label>
                      </div>
                      <div className="form-outline mb-4">
                        <input
                          name="password"
                          type="password"
                          id="passwordInput1"
                          className="form-control form-control-lg"
                          value={user.password}
                          onChange={onFormFieldChange}
                        />
                        <label className="form-label" htmlFor="password">
                          Password
                        </label>
                      </div>
                      <div className="pt-1 mb-4">
                        <button
                          onClick={loginHandler}
                          name="loginBtn"
                          className="btn btn-info btn-lg btn-block"
                          type="button"
                          id="loginBtn"
                        >
                          Login
                        </button>
                      </div>
                    </form>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>
    </React.Fragment>
  );
}

export default Login;
