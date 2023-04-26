import React, { Component } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faAdd } from '@fortawesome/free-solid-svg-icons'

export class Item extends Component {

    constructor(props) {
        super(props);
        this.state = {
            menuItem: null
        }
    }

    render() {
        return (
            <div className="menu-item" v-for="item in items">
                <a className="thumbnail" href="#">
                    <img src={this.props.menuItem?.url} alt={this.props.menuItem?.title} loading='lazy' />
                </a>
                <div className="info">
                    <a className="title" href="#">{this.props.menuItem?.title}</a>
                    <div className="desc">{this.props.menuItem?.desc}</div>
                    <div className="price">
                        <div className="price-color">
                            {this.props.menuItem?.priceWithComma}
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-sm-12">
                            <div className="input-group">
                                <div className="input-group-prepend">
                                    <span className="input-group-text substract" style= {{cursor: 'pointer'}}>-</span>
                                </div>
                                <input ref="quant" style={{maxWidth: '50px', textalign: 'center'}} type="number" inputMode="numeric" className="form-control quantity" min="1" value="1" />
                                <div className="input-group-prepend">
                                    <span className="input-group-text plus" style={{cursor: 'pointer'}}>+</span>
                                </div>
                                <div className="input-group-append">
                                    <button type="button" className="btn btn-info btnAdd"><i className="fa fa-cart-plus"></i>Thêm</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        )
    }
}
