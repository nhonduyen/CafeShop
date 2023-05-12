import React, { useContext } from 'react'
import { CategoriesContext } from './Home'

export function NavMenu() {
  const { categoriesList } = useContext(CategoriesContext)

  return (
    <nav className="container-fluid sticky-top" id="menu1">
      <section className="container" id="horizon-menu1">
        <div className="row" id="category-container">
          <div className="scrollmenu col" data-simplebar>
            <ul>
              <li id='all'><a className='list-group-item side-group-item'>Tất cả</a></li>
              {
                categoriesList.map(cate => (
                  <li key={cate.cateId}><a className='list-group-item side-group-item'>{cate.name}</a></li>
                ))
              }
            </ul>
          </div>
        </div>

      </section>
    </nav>
  )
}
