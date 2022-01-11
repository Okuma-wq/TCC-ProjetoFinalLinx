import React from 'react';
import {DateTimePickerComponent} from '@syncfusion/ej2-react-calendars';

function App() {
  const dateValue: Date = new Date("01/01/2022 10:30 AM");
  const minDate: Date = new Date("01/01/2022 09:00 AM");
  const maxDate: Date = new Date("01/05/2022 06:00 PM");
  return (
    <div>
      <DateTimePickerComponent placeholder="Choose a date and time"
      value={dateValue}
      min={minDate}
      max={maxDate}
      format="dd-MM-yy HH:mm:ss"
      step={60}></DateTimePickerComponent>
    </div>
  );
}

export default App;