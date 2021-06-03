// import { notification } from "antd";
// import { message } from 'antd';
// import { confirmAlert, ReactConfirmAlertProps } from "react-confirm-alert";

// export function errorAlert(content: string, duration: number = 1.5) {
//     message.error(content, duration);
// }

// export function successAlert(content: string, duration: number = 1.5) {
//     appAlert('success', content, duration);
// }

// export function infoAlert(content: string, duration: number = 1.5) {
//     appAlert('info', content, duration);
// }

// export function warningAlert(content: string, duration: number = 1.5) {
//     appAlert('warning', content, duration);
// }

export function errorAlert(message: string, duration: number = 1.5) {
    appAlert('error', message, duration);
}

export function successAlert(message: string, duration: number = 1.5) {
    appAlert('success', message, duration);
}

export function infoAlert(message: string, duration: number = 1.5) {
    appAlert('info', message, duration);
}

export function warningAlert(message: string, duration: number = 1.5) {
    appAlert('warning', message, duration);
}

function appAlert(type: string, message: string, duration: number = 1.5, title?: string) {
    // notification[type]({
    //     message: title,
    //     description: message,
    //     placement: 'bottomRight',
    //     duration: duration
    // });

    alert(message);
}

// export function messageConfig() {
//     message.config({
//         top: window.screen.height - 100,
//         duration: 1.5,
//         maxCount: 3,
//         rtl: true
//     });
// }

// export function appConfirm(message: string, title: string, onYes: any) {
//     var options: ReactConfirmAlertProps = {
//         title: title,
//         message: message,
//         buttons: [
//             {
//                 label: 'Yes',
//                 onClick: () => {onYes();}
//             },
//             {
//                 label: 'No',
//                 onClick: () => {}
//             }
//         ],
//         closeOnEscape: true,
//         closeOnClickOutside: true,
//         willUnmount: () => { },
//         onClickOutside: () => { },
//         onKeypressEscape: () => { },
//         overlayClassName: "overlay-custom-class-name"
//     };

//     confirmAlert(options);
// }