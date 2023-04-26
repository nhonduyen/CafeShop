import React, { Component } from 'react';
import { Container } from 'reactstrap';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <React.Fragment>
        <Container fluid>
          {this.props.children}
        </Container>
      </React.Fragment>
    );
  }
}
