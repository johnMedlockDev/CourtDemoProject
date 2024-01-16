import { useRouter } from 'next/router'

function Participant () {
	const router = useRouter()
	const { participantId } = router.query
}

export default Participant
