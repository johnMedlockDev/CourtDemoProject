import { useRouter } from 'next/router'

function Detail () {
	const router = useRouter()
	const { detailId } = router.query
}

export default Detail
