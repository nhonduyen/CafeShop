import React, { Component } from 'react';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <React.Fragment>
          {this.props.children}
      </React.Fragment>
    );
  }
}
