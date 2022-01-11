import React, { Component, useEffect, useState } from "react"
import { StyleSheet, View, Text, Pressable, TextInput, TouchableOpacity, Image } from "react-native"
import AsyncStorage from '@react-native-async-storage/async-storage'
import '../../App.css'

import DateTime from '../components/datetime'
import jwtDecode from 'jwt-decode'
import { DateTimePickerComponent } from '@syncfusion/ej2-react-calendars';

import api from '../services/api'
import axios from "axios"


export const Editar = ({ route, params }) => {
    const [dataReuniao, setDataReuniao] = useState('')
    const [local, setLocal] = useState('')

    const editarReuniao = () => {
        axios.patch('http://localhost:5000/api/Reuniao/Minhas/' + route.params.id + '/Alterar', {
            dataReuniao: dataReuniao,
            local: local,
            status: 0
        }, {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('userToken')
            }
        })

        

            .then(resposta => {
                if (resposta.status !== 200) {
                    throw Error();
                }

                console.log('Reunião Atualizada')
            })

            
            .catch(erro => console.log(erro));

        }





    return (
        <View style={styles.main}>
            <View style={styles.container} >
                <View style={styles.mainHeader}>
                    <View style={styles.mainHeaderRow}>
                        <Text style={styles.mainHeaderText}>{"Editar Reunião".toUpperCase()}</Text>
                    </View>
                    <View style={styles.mainHeaderLine} />
                </View>

                <View style={styles.mainBody}>
                    <View style={styles.input}>

                        <DateTimePickerComponent placeholder=""
                            // value={dateValue}
                            // min={minDate}
                            // max={maxDate}
                            format="dd-MM-yyyy HH:mm:ss"
                            step={60}
                            onChange={event => setDataReuniao(event.target.value)}
                        />

                        <TextInput
                            placeholder="Local"
                            placeholderTextColor="rgba(0,0,0,1)"
                            style={styles.input}
                            onChangeText={local => setLocal(local)}
                        ></TextInput>


                        <TouchableOpacity
                            onPress={editarReuniao}
                            style={styles.button}
                            
                        >
                            <Text style={styles.textButton}>Editar</Text>
                        </TouchableOpacity>
                    </View>
                </View>
            </View>
        </View>
    )
}

const styles = StyleSheet.create({

    container: {
        flex: 1,
        backgroundColor: 'BLACK',
        borderRadius: 50,
    },

    main: {
        flex: 1,
        backgroundColor: '#F1F1F1',
        borderRadius: 50,
    },

    mainHeader: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        backgroundColor: '#FFB200'
    },
    mainHeaderRow: {
        flexDirection: 'row'
    },
    // imagem do cabeçalho
    mainHeaderImg: {
        width: 22,
        height: 22,
        tintColor: '#ccc',
        marginRight: -5,
        marginTop: -12
    },
    // texto do cabeçalho
    mainHeaderText: {
        fontSize: 16,
        letterSpacing: 5,
        color: '#000000',
    },
    // linha de separação do cabeçalho
    mainHeaderLine: {
        width: 220,
        paddingTop: 10,
        borderBottomColor: '#000000',
        borderBottomWidth: 1
    },

    mainBody: {
        flex: 4,
        backgroundColor: '#FFB200'
      },

      mainBodyContent: {
        paddingTop: 5,
        paddingRight: 40,
        paddingLeft: 40,
        marginTop: 5,
        marginBottom: 18,
        // backgroundColor: 'lightgreen'
      },

      input: {
        fontFamily: "nunito-regular.ttf",
        color: "#121212",
        height: 42,
        width: '70%',
        borderWidth: 2,
        borderColor: "rgba(FF,FF,FFF,F)",
        backgroundColor: "#F5F7F9",
        borderRadius: 5,
        borderStyle: "solid",
        marginTop: 20,
        paddingLeft: 20
    },


})

export default Editar;