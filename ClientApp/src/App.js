import React, { Component } from "react";
import { Route } from "react-router";
import { Layout } from "./components/Layout";
import Home from "./components/Home";
import { FetchData } from "./components/FetchData";
import { Counter } from "./components/Counter";

import "./custom.css";
import LoginForm from "./components/LoginForm";
import config from "./config.json";
import auth from "./services/authService";
import { ToastContainer } from "react-toastify";
import StudentDetails from "./components/StudentDetails";
import Classrooms from "./components/Classrooms";
import Courses from "./components/Courses";

export default class App extends Component {
  static displayName = config.displayName;
  state = {};
  componentDidMount() {
    const user = auth.getCurrentUser();
    console.log("User", user);
    this.setState({ user });
  }

  render() {
    const { user } = this.state;
    return (
      <>
        {user && (
          <Layout>
            <Route exact path="/" component={Home} />
            <Route exact path="/home" component={Home} />
            <Route path="/classrooms" component={Classrooms} />
            <Route path="/courses" component={Courses} />
            <Route path="/student-details/:id" component={StudentDetails} />
            <Route path="/student-details" component={StudentDetails} />
          </Layout>
        )}
        {!user && <LoginForm></LoginForm>}
      </>
    );
  }
}
