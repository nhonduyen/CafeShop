import React, { useContext } from 'react';
import { ItemsContext } from './Home'

export function Item(props) {
    const { isShowItemDetails, setIsShowItemDetails } = useContext(ItemsContext)


    return (
        <div className="menu-item">
            <a className="thumbnail" href="#" onClick={() => console.log('show')}>
                <img src={props.menuItem?.url} alt={props.menuItem?.title} loading='lazy' />
            </a>
            <div className="info">
                <a className="title" href="#" onClick={() => () => console.log('show') }>{props.menuItem?.title}</a>
                <div className="desc">{props.menuItem?.desc}</div>
                <div className="price">
                    <div className="price-color">
                        {props.menuItem?.priceWithComma}
                    </div>
                </div>
                <div className="row">
                    <div className="col-sm-12">
                        <div className="input-group">
                            <div className="input-group-prepend">
                                <span className="input-group-text substract" style={{ cursor: 'pointer' }}>-</span>
                            </div>
                            <input style={{ maxWidth: '50px', textalign: 'center' }} type="number" inputMode="numeric" className="form-control quantity" min="1" defaultValue={1} />
                            <div className="input-group-prepend">
                                <span className="input-group-text plus" style={{ cursor: 'pointer' }}>+</span>
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
