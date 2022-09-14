import React, { Component } from "react";
import { toast } from "react-toastify";
import { Button, Popover, PopoverHeader, PopoverBody, Toast } from "reactstrap";
import { list } from "../services/classroomService";

export class Counter extends Component {
  constructor(props) {
    super(props);

    this.toggle = this.toggle.bind(this);
    this.state = {
      popoverOpen: false,
    };
  }

  toggle() {
    this.setState({
      popoverOpen: !this.state.popoverOpen,
    });
  }

  something = async () => {
    var f = await list();
    console.log(f);
  };

  anotherSomething = () => {
    toast.success("okay I can do this");
  };

  render() {
    return (
      <div>
        <Button id="Popover1" onClick={this.something}>
          Launch Popover
        </Button>

        <Button id="Popover1" onClick={this.anotherSomething}>
          Toast
        </Button>
        <Popover
          placement="bottom"
          isOpen={this.state.popoverOpen}
          target="Popover1"
          toggle={this.toggle}
        >
          <PopoverHeader>Popover Title</PopoverHeader>
          <PopoverBody>
            Sed posuere consectetur est at lobortis. Aenean eu leo quam.
            Pellentesque ornare sem lacinia quam venenatis vestibulum.
          </PopoverBody>
        </Popover>
      </div>
    );
  }
}
