import React, { Component } from "react";
import { ToastContainer } from "react-toastify";
import { Container } from "reactstrap";
import { NavMenu } from "./NavMenu";

export class Layout extends Component {
  static displayName = Layout.name;

  render() {
    return (
      <div>
        <NavMenu />
        <ToastContainer />
        <Container>{this.props.children}</Container>
      </div>
    );
  }
}
