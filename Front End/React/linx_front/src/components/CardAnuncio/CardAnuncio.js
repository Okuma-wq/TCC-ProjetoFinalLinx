import React, { useState } from 'react'
import { useHistory } from 'react-router-dom'

import './CardAnunciostyle.css'


export const CardAnuncio = (props) => {

    const [anuncio, setAnuncio] = useState(props.anuncio)

    const history = useHistory();

    const buscarId = (id) =>  {
        history.push({
          pathname: `/anuncio/${id}`
        })
      }

    return(
        <div className='card'>
            <div className='CardContainer' onClick={() => buscarId(anuncio.id)}>
                <h4>{anuncio.titulo}</h4>
                <p className='cardDescricao'>{anuncio.descricao}</p>
                <p className='visualizarAnuncio'>Visualizar Anuncio</p>
            </div>
        </div>
    )
}

